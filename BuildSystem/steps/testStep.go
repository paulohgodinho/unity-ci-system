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

	fmt.Println("This text came from inside Test Step")
	fmt.Printf("This build started at %v \n", executionContext.StartedAt)
	fmt.Println(step.WhatToSay)

	return 0 // All was right
}
