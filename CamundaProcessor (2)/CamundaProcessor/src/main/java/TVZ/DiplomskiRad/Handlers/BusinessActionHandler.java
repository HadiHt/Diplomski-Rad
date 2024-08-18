package TVZ.DiplomskiRad.Handlers;

import TVZ.DiplomskiRad.Models.BusinessRequestModel;
import TVZ.DiplomskiRad.Models.DataModel;
import TVZ.DiplomskiRad.Models.GenericResponseModel;
import TVZ.DiplomskiRad.Services.CamundaProcessService;
import TVZ.DiplomskiRad.Services.ProcessStarter;

import org.camunda.bpm.engine.RuntimeService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.stereotype.Component;
import org.camunda.bpm.client.ExternalTaskClient;
import org.camunda.bpm.client.task.ExternalTaskHandler;
import org.camunda.bpm.client.task.ExternalTask;
import org.camunda.bpm.client.task.ExternalTaskService;

import com.fasterxml.jackson.databind.ObjectMapper;

import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.web.client.RestTemplate;
import com.google.gson.Gson;



@Component
public class BusinessActionHandler implements ExternalTaskHandler{
    private final CamundaProcessService camundaProcessService;
    private static final Logger log = LoggerFactory.getLogger(ProcessStarter.class);
    private static final String url = "http://localhost:5041/BusinessAction/Execute-Action";
    public BusinessActionHandler(CamundaProcessService camundaProcessService){
        this.camundaProcessService = camundaProcessService;
    }
    @Override
    public void execute(ExternalTask externalTask, ExternalTaskService externalTaskService) {

        log.info("Executing ExternalTask Logic Started!");
        Gson gson = new Gson();
        DataModel data = new DataModel();
        data.setCourseName(externalTask.getVariable("Course"));
        data.setMajor(externalTask.getVariable("Major"));
        data.setEmail(externalTask.getVariable("Email"));
        data.setFullName(externalTask.getVariable("FullName"));
        data.setDateOfBirth(externalTask.getVariable("DateOfBirth"));

        try {
            BusinessRequestModel request = new BusinessRequestModel();
            request.setWoid(externalTask.getBusinessKey());
            request.setTaskId(externalTask.getActivityId());
            request.setOrderType("test");
            request.setProcessDefinitionKey(externalTask.getProcessDefinitionKey());
            request.setProcessInstanceId(externalTask.getProcessInstanceId());
            request.setData(gson.toJson(data));

            // Create RestTemplate with custom HttpClient
            RestTemplate restTemplate = new RestTemplate();
            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(org.springframework.http.MediaType.APPLICATION_JSON);
            HttpEntity<BusinessRequestModel> requestEntity = new HttpEntity<>(request, headers);

            try {
                ResponseEntity<String> response = restTemplate.exchange(url, HttpMethod.POST, requestEntity, String.class);
                log.info("Response from BusinessAction endpoint: " + response.getBody());
                ObjectMapper objectMapper = new ObjectMapper();
                GenericResponseModel genericResponse = objectMapper.readValue(response.getBody(), GenericResponseModel.class);
                if(genericResponse.getStatus() == 0){
                }
                else{
                }
            } catch (Exception ex) {
                log.error("Failed calling BusinessAction Endpoint with error: " + ex);
            }
        } catch (Exception ex) {
            log.error("Failed in method with error: " + ex);
        }

        externalTaskService.complete(externalTask);
    }
}
