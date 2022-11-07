package gol_test

import (
	"gol/gol"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestRulesForLivingCells(t *testing.T) {
	tests := []struct {
		name string

		currentState      gol.CellState
		livingNeighbors   int64
		expectedNextState gol.CellState
	}{
		{
			name:              "too few living neighbors (0) dies",
			currentState:      gol.Alive,
			livingNeighbors:   0,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too few living neighbors (1) dies",
			currentState:      gol.Alive,
			livingNeighbors:   1,
			expectedNextState: gol.Dead,
		},
		{
			name:              "enough living neighbors to survive (2)",
			currentState:      gol.Alive,
			livingNeighbors:   2,
			expectedNextState: gol.Alive,
		},
		{
			name:              "enough living neighbors to survive (3)",
			currentState:      gol.Alive,
			livingNeighbors:   3,
			expectedNextState: gol.Alive,
		},
		{
			name:              "too many living neighbors to survive (4)",
			currentState:      gol.Alive,
			livingNeighbors:   4,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (5)",
			currentState:      gol.Alive,
			livingNeighbors:   5,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (6)",
			currentState:      gol.Alive,
			livingNeighbors:   6,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (7)",
			currentState:      gol.Alive,
			livingNeighbors:   7,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (8)",
			currentState:      gol.Alive,
			livingNeighbors:   8,
			expectedNextState: gol.Dead,
		},
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {

			actual := gol.NextState(tt.currentState, tt.livingNeighbors)

			assert.Equal(t, actual, tt.expectedNextState)
		})
	}
}
