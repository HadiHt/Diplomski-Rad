package TVZ.DiplomskiRad.Services;

import org.camunda.bpm.engine.RuntimeService;
import org.springframework.stereotype.Service;

@Service
public class CamundaProcessService {

    private final RuntimeService runtimeService;

    public CamundaProcessService(RuntimeService runtimeService) {
        this.runtimeService = runtimeService;
    }
    public void addVariableToProcessInstance(String processInstanceId, String variableName, Object value) {
        runtimeService.setVariable(processInstanceId, variableName, value);
    }
}