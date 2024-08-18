package TVZ.DiplomskiRad.Handlers;

import TVZ.DiplomskiRad.Services.CamundaProcessService;
import org.camunda.bpm.client.ExternalTaskClient;
import org.camunda.bpm.client.backoff.ExponentialBackoffStrategy;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class BusinessActionHandlerConfigurator {
    private final CamundaProcessService camundaProcessService;
    public BusinessActionHandlerConfigurator(CamundaProcessService camundaProcessService){
        this.camundaProcessService = camundaProcessService;
    }
    @Bean
    public ExternalTaskClient externalTaskClient() {
        ExternalTaskClient client = ExternalTaskClient.create()
                .baseUrl("http://localhost:8081/engine-rest")
                .build();

        client.subscribe("BusinessAction")
                .handler(new BusinessActionHandler(camundaProcessService))
                .open();

        return client;
    }
}
