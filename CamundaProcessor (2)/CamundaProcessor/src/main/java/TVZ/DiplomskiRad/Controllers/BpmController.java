package TVZ.DiplomskiRad.Controllers;


import TVZ.DiplomskiRad.Models.ApplicationModel;
import TVZ.DiplomskiRad.Services.ProcessStarter;
import org.camunda.bpm.engine.runtime.ProcessInstance;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class BpmController {
    private final ProcessStarter processStarter;
    private static final Logger log = LoggerFactory.getLogger(ProcessStarter.class);

    @Autowired
    public BpmController(ProcessStarter processStarter) {
        this.processStarter = processStarter;
    }

    @PostMapping("/start")
    public void startProcess(@RequestBody ApplicationModel applicationForm) {
        log.info("Triggered StartProcess Endpoint For WOID: "+ applicationForm.getWoid());
        processStarter.StartInstance(applicationForm);
    }
}
