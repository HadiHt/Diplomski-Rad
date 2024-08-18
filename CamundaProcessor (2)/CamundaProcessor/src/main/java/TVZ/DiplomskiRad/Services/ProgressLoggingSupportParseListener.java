package TVZ.DiplomskiRad.Services;

import org.camunda.bpm.engine.impl.bpmn.behavior.UserTaskActivityBehavior;
import org.camunda.bpm.engine.impl.bpmn.parser.AbstractBpmnParseListener;
import org.camunda.bpm.engine.impl.pvm.delegate.ActivityBehavior;
import org.camunda.bpm.engine.impl.pvm.process.ActivityImpl;
import org.camunda.bpm.engine.impl.pvm.process.ScopeImpl;
import org.camunda.bpm.engine.impl.util.xml.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ProgressLoggingSupportParseListener extends AbstractBpmnParseListener {

    private static final Logger log = LoggerFactory.getLogger(ProcessStarter.class);

/*    @Override
    public void parseUserTask(Element userTaskElement, ScopeImpl scope, ActivityImpl activity) {
        ActivityBehavior activityBehavior = activity.getActivityBehavior();
        log.info("WINNNNNN");
        if(activityBehavior instanceof UserTaskActivityBehavior){
            UserTaskActivityBehavior userTaskActivityBehavior = (UserTaskActivityBehavior) activityBehavior;
            log.info("Inside Parse User Mathod" + userTaskActivityBehavior.getTaskDefinition().getKey());
        }
    }*/


	/*@Override
	public void parseUserTask(Element userTaskElement, ScopeImpl scope, ActivityImpl activity) {
		StartListener startListener = new StartListener();
		activity.addListener(ExecutionListener.EVENTNAME_START, startListener);
	}*/
}
