const willCellLive = (isAlive, livingNeighborsCount) => {
    if (isAlive && livingNeighborsCount === 2) {
        return true
    }
    if (livingNeighborsCount === 3) {
        return true
    }
    return false
}

const createNewGrid = () => []

const createLiveCell = (column, row) => ({column, row})

const setLiveCells = (grid, cells) => grid.concat(cells)

const getLiveNeighborCount = (grid, column, row) => grid.filter(cell => {
    const columnDelta = Math.abs(cell.column - column)
    const rowDelta = Math.abs(cell.row - row)

    return columnDelta <= 1 && rowDelta <= 1 && columnDelta + rowDelta != 0
}).length