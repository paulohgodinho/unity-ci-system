package steps

import (
	"buildsystem/base"
	"fmt"
)

type SampleStep struct {
}

func (step SampleStep) Execute(executionContext base.ExecutionContext) int {
	fmt.Println("Executing Step")

	return 0
}

func (step SampleStep) StepData() base.StepData {
	return base.StepData{
		Name: "Sample Step",
	}
}
