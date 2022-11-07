package gol

type CellState int64

const (
	Alive CellState = iota
	Dead
)

func NextState(c CellState, n int64) CellState {
	if n == 3 {
		return Alive
	}
	if n == 2 {
		return c
	}
	return Dead
}
