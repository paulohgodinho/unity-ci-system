package steps

import (
	"buildsystem/base"
	"fmt"
)

type UploadToS3Step struct {
}

func (step UploadToS3Step) Execute(executionContext base.ExecutionContext) int {
	fmt.Println("Mock Uploading to S3...")

	return 0
}

func (step UploadToS3Step) StepData() base.StepData {
	return base.StepData{
		Name: "Upload to S3",
	}
}
