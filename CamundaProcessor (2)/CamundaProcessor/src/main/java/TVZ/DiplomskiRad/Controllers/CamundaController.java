package TVZ.DiplomskiRad.Controllers;

import TVZ.DiplomskiRad.Models.ApplicationModel;
import TVZ.DiplomskiRad.Models.CamundaControllerModel;
import TVZ.DiplomskiRad.Services.CamundaProcessService;
import TVZ.DiplomskiRad.Services.ProcessStarter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/Camunda/api")
public class CamundaController {
    private static final Logger log = LoggerFactory.getLogger(ProcessStarter.class);
    private final CamundaProcessService camundaProcessService;

    @Autowired
    public CamundaController(CamundaProcessService camundaProcessService) {
        this.camundaProcessService = camundaProcessService;
    }

    @PostMapping("/addVariable")
    public void addVariable(@RequestBody CamundaControllerModel camundaModel) {
        log.info("Triggered addVariable Endpoint For WOID: "+ camundaModel.getWoid());
        camundaProcessService.addVariableToProcessInstance(camundaModel.getProcessInstanceId(),camundaModel.getVariableName(),camundaModel.getVariableValue());
    }
}