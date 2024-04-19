# unity-ci-system
This is a Proof of Concept CI system that has a dedicated step to integrate with Unity, it can also be used to automate the building and deploy of any stack.

## Components
### CI System
The CI system written in Go that can run any number of steps in sequence, these steps can use the full power of Go by including any required library from the vast collection provided by Go repositories.

### Unity Integration
An integration added to the Unity project makes it easy to build using the CI system but also allows builds from inside Unity to be quick and easy **by leveraging Build Templates** that can be commited to VCS and shared with other developers.

## CI System
The CI system can be found in folder `./BuildSystem` an can be started by running your current platform binary, the executable has the following na ing convention `buildSystem_OS_ARCH`, it can be run directly from the command line.

If no step requires human input the pipeline will run its course automatically.

Sample pipeline configured in `main.go`
```go
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
```
Running this pipeline yields the following output:
```
┌────────────────────────────────────────────────────────────────────────────────────────────────────┐
│                                      Welcome to the CI System                                      │
└────────────────────────────────────────────────────────────────────────────────────────────────────┘
Starting Steps Execution
 >  Executing step: Test Step
  --  This text came from inside Test Step
  --  This build started at 2024-04-19T14:44:38-03:00
  --  Hi I am a Test Step!
 >  Finished step: Test Step
 >  Executing step: Wait for Seconds Step
  --  Waiting for 3 seconds
 >  Finished step: Wait for Seconds Step
 >  Executing step: Upload to Steam
  --  Moc uploading to Steam...
 >  Finished step: Upload to Steam
 >  Executing step: Upload to S3
  --  Mock Uploading to S3...
 >  Finished step: Upload to S3
 >  Executing step: Test Step
  --  This text came from inside Test Step
  --  This build started at 2024-04-19T14:44:38-03:00
  --  Another Test Step!
 >  Finished step: Test Step
 >  Executing step: Open Target Step
  --  Opening target folder ../buildOutput
 >  Finished step: Open Target Step
Finished, press ENTER to exit...
 ```