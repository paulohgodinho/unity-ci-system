package main

import (
	"buildsystem/base"
	"buildsystem/executor"
	"buildsystem/steps"
	"fmt"
	"time"
)

func main() {

	base.PrintWelcome()

	var executionContext base.ExecutionContext = base.ExecutionContext{
		ProjectName: "Custom CI",
		StartedAt:   time.Now().Format(time.RFC3339),
	}
	executor.SetupExecutor(executionContext)

	executor.AddStep(steps.TestStep{WhatToSay: "Hi I am a Test Step!"})
	executor.AddStep(steps.WaitForSecondsStep{SecondsToWaitFor: 3})
	executor.AddStep(steps.UploadToSteamStep{})
	executor.AddStep(steps.UploadToS3Step{})
	executor.AddStep(steps.TestStep{WhatToSay: "Another Test Step!"})
	executor.AddStep(steps.OpenTargetFolderStep{TargetFolder: "../buildOutput"})

	executor.ExecuteSteps()

	fmt.Println("Finished, press ENTER to exit...")
	fmt.Scanln()

	//executor.AddStep(steps.BuildUnityGameStep{})
}
