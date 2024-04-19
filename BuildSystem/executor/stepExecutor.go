package executor

import (
	"buildsystem/base"
	"fmt"
)

var stepList []base.Step
var executionContext base.ExecutionContext

func SetupExecutor(execContext base.ExecutionContext) {
	executionContext = execContext
}

func AddStep(step base.Step) {
	stepList = append(stepList, step)
}

func ExecuteSteps() {
	fmt.Println("Starting Steps Execution")

	for _, step := range stepList {
		base.PrintStepText(fmt.Sprintf("Executing step: %s", step.StepData().Name))
		step.Execute(executionContext)
		base.PrintStepText(fmt.Sprintf("Finished step: %s", step.StepData().Name))
		//base.PrintDelimiter()
	}
}
