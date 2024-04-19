package main

import (
	"buildsystem/base"
	"buildsystem/executor"
	"buildsystem/steps"
	"fmt"
	"time"
)

func main() {

	var executionContext base.ExecutionContext = base.ExecutionContext{
		ProjectName: "Custom CI",
		StartedAt:   time.Now().Format(time.RFC3339),
	}

	executor.SetupExecutor(executionContext)
	executor.AddStep(steps.TestStep{WhatToSay: "Hi I am a Test Step!"})
	//executor.AddStep(steps.WaitForSecondsStep{SecondsToWaitFor: 1})
	executor.AddStep(steps.BuildUnityGameStep{})
	executor.AddStep(steps.TestStep{WhatToSay: "Another Test Step!"})
	executor.AddStep(steps.OpenTargetFolderStep{TargetFolder: "../buildOutput"})

	executor.ExecuteSteps()

	fmt.Println("Finished, press ENTER to exit...")
	fmt.Scanln()
}
