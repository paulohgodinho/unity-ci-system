package steps

import (
	"buildsystem/base"
	"fmt"
	"time"
)

type WaitForSecondsStep struct {
	SecondsToWaitFor int
}

func (step WaitForSecondsStep) StepData() base.StepData {
	return base.StepData{
		Name: "Wait for Seconds Step",
	}
}

func (step WaitForSecondsStep) Execute(executionContext base.ExecutionContext) int {
	fmt.Printf("Waiting for %v", step.SecondsToWaitFor)
	time.Sleep(time.Duration(step.SecondsToWaitFor) * time.Second)

	return 0
}
