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
		fmt.Print("Executing step: ")
		fmt.Println(step.StepData().Name)
		step.Execute(executionContext)
		fmt.Print("Finished executing step")
		fmt.Print(step.StepData().Name)
	}
}
