package TVZ.DiplomskiRad.Services;

import TVZ.DiplomskiRad.Models.ApplicationModel;
import org.camunda.bpm.engine.RuntimeService;
import org.camunda.bpm.engine.runtime.ProcessInstance;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.Map;

@Service
public class ProcessStarter {
    private final RuntimeService runtimeService ;
    private static final Logger log = LoggerFactory.getLogger(ProcessStarter.class);

    @Autowired
    public ProcessStarter (RuntimeService runtimeService) {
        this . runtimeService = runtimeService ;
    }

    public Boolean StartInstance(ApplicationModel applicationForm){
            Map<String, Object> variables = new HashMap<>();
            variables.put("Woid", applicationForm.getWoid());
            variables.put("OrderType", applicationForm.getOrderType());
            variables.put("FullName", applicationForm.getFullName());
            variables.put("Email", applicationForm.getEmail());
            variables.put("DateOfBirth", applicationForm.getDateOfBirth());
            variables.put("jmbag", applicationForm.getJmbag());
            variables.put("Major", applicationForm.getMajor());
            variables.put("Course",applicationForm.getCourse());
            variables.put("application-time", LocalDateTime.now());

            ProcessInstance processInstance = runtimeService.startProcessInstanceByKey(applicationForm.getProcessDefinitionKey(), applicationForm.getWoid(), variables);
            Object processId = runtimeService.getVariable(processInstance.getId(), "PROCESS_ID");

            log.info("Created new instance with ID: {} and BUSINESS_KEY: {}", processInstance.getId(), applicationForm.getWoid());
            return true;
    }
    }
