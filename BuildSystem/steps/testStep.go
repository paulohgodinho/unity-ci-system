package steps

import (
	"buildsystem/base"
	"fmt"
)

type TestStep struct {
	WhatToSay string
}

func (step TestStep) StepData() base.StepData {
	return base.StepData{Name: "Test Step"}
}

func (step TestStep) Execute(executionContext base.ExecutionContext) int {

	base.Printl("This text came from inside Test Step")
	base.Printl(fmt.Sprintf("This build started at %v", executionContext.StartedAt))
	base.Printl(step.WhatToSay)

	return 0 // All was right
}
