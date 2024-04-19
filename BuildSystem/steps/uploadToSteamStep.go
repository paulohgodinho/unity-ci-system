package steps

import (
	"buildsystem/base"
)

type UploadToSteamStep struct {
}

func (step UploadToSteamStep) Execute(executionContext base.ExecutionContext) int {
	base.Printl("Moc uploading to Steam...")

	return 0
}

func (step UploadToSteamStep) StepData() base.StepData {
	return base.StepData{
		Name: "Upload to Steam",
	}
}
