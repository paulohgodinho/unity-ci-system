package base

type Step interface {
	Execute(executionContext ExecutionContext) int
	StepData() StepData
}

type StepData struct {
	Name string
}
