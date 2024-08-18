package TVZ.DiplomskiRad.Config;

import TVZ.DiplomskiRad.Models.ApplicationModel;
import TVZ.DiplomskiRad.Controllers.BpmController;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.rabbit.annotation.EnableRabbit;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer;
import org.springframework.amqp.rabbit.listener.api.ChannelAwareMessageListener;
import org.springframework.amqp.rabbit.listener.api.ChannelAwareMessageListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

@Configuration
@EnableRabbit
public class RabbitMQConfig {


    @Autowired
    private BpmController bpmController;

    @Bean
    public Queue queue() {
        return new Queue("CamundaQueue", true);
    }

    @Bean
    public SimpleMessageListenerContainer messageListenerContainer(ConnectionFactory connectionFactory) {
        SimpleMessageListenerContainer container = new SimpleMessageListenerContainer();
        container.setConnectionFactory(connectionFactory);
        container.setQueueNames("CamundaQueue");
        container.setMessageListener(new ChannelAwareMessageListener() {
            @Override
            public void onMessage(org.springframework.amqp.core.Message message, com.rabbitmq.client.Channel channel) throws Exception {
                String msg = new String(message.getBody());
                System.out.println("Received Message: " + msg);

                ObjectMapper mapper = new ObjectMapper();
                JsonNode rootNode = mapper.readTree(msg);

                ApplicationModel model = new ApplicationModel();

                model.setWoid(rootNode.path("Woid").asText());
                model.setGuid(rootNode.path("newGuid").asText());
                model.setProcessDefinitionKey(rootNode.path("processDefinitionKey").asText());
                model.setOrderType(rootNode.path("OrderType").asText());
                model.setMajor(rootNode.path("Major").asText());
                model.setCourse(rootNode.path("course").asText());

                JsonNode userObjectNode = rootNode.path("UserObject");

                model.setFullName(userObjectNode.path("name").asText());
                model.setEmail(userObjectNode.path("email").asText());
                model.setDateOfBirth(userObjectNode.path("dateOfBirth").asText());
                model.setOib(String.valueOf(userObjectNode.path("oib").asInt()));

                bpmController.startProcess(model);
                try{
                    channel.basicAck(message.getMessageProperties().getDeliveryTag(), false);
                }catch(Exception ex){
                    System.out.println(ex);
                }
            }
        });
        return container;
    }

    @Bean
    public RabbitTemplate rabbitTemplate(ConnectionFactory connectionFactory) {
        return new RabbitTemplate(connectionFactory);
    }
}
