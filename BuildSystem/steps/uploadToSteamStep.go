package steps

import (
	"buildsystem/base"
	"fmt"
)

type UploadToSteamStep struct {
}

func (step UploadToSteamStep) Execute(executionContext base.ExecutionContext) int {
	fmt.Println("Moc uploading to Steam...")

	return 0
}

func (step UploadToSteamStep) StepData() base.StepData {
	return base.StepData{
		Name: "Upload to Steam",
	}
}
