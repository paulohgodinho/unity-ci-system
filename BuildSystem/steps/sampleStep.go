package steps

import (
	"buildsystem/base"
)

type SampleStep struct {
}

func (step SampleStep) Execute(executionContext base.ExecutionContext) int {
	base.Printl("Executing Sample Step")

	return 0
}

func (step SampleStep) StepData() base.StepData {
	return base.StepData{
		Name: "Sample Step",
	}
}
