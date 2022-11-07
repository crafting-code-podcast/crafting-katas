package gol_test

import (
	"gol/gol"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestRulesForLivingCells(t *testing.T) {
	tests := []struct {
		name string

		livingNeighbors   int64
		expectedNextState gol.CellState
	}{
		{
			name:              "too few living neighbors (0) dies",
			livingNeighbors:   0,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too few living neighbors (1) dies",
			livingNeighbors:   1,
			expectedNextState: gol.Dead,
		},
		{
			name:              "enough living neighbors to survive (2)",
			livingNeighbors:   2,
			expectedNextState: gol.Alive,
		},
		{
			name:              "enough living neighbors to survive (3)",
			livingNeighbors:   3,
			expectedNextState: gol.Alive,
		},
		{
			name:              "too many living neighbors to survive (4)",
			livingNeighbors:   4,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (5)",
			livingNeighbors:   5,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (6)",
			livingNeighbors:   6,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (7)",
			livingNeighbors:   7,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors to survive (8)",
			livingNeighbors:   8,
			expectedNextState: gol.Dead,
		},
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {

			actual := gol.NextState(gol.Alive, tt.livingNeighbors)

			assert.Equal(t, actual, tt.expectedNextState)
		})
	}
}

func TestRulesForDeadCells(t *testing.T) {
	tests := []struct {
		name string

		livingNeighbors   int64
		expectedNextState gol.CellState
	}{
		{
			name:              "too few living neighbors (0) stays dead",
			livingNeighbors:   0,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too few living neighbors (1) stays dead",
			livingNeighbors:   1,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too few living neighbors (2) stays dead",
			livingNeighbors:   2,
			expectedNextState: gol.Dead,
		},
		{
			name:              "enough living neighbors to revive (3)",
			livingNeighbors:   3,
			expectedNextState: gol.Alive,
		},
		{
			name:              "too many living neighbors (4) stays dead",
			livingNeighbors:   4,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors (5) stays dead",
			livingNeighbors:   5,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors (6) stays dead",
			livingNeighbors:   6,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors (7) stays dead",
			livingNeighbors:   7,
			expectedNextState: gol.Dead,
		},
		{
			name:              "too many living neighbors (8) stays dead",
			livingNeighbors:   8,
			expectedNextState: gol.Dead,
		},
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {

			actual := gol.NextState(gol.Dead, tt.livingNeighbors)

			assert.Equal(t, actual, tt.expectedNextState)
		})
	}
}
