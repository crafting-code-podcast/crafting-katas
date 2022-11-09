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

const setLiveCells = (grid, cells) => {
    const deduped = []
    grid.concat(cells).forEach(x => {
        if (!deduped.some(y => y.column == x.column && y.row == x.row)) {
            deduped.push(x)
        }
    })
    return deduped
}

const getLiveNeighborCount = (grid, column, row) => grid.filter(cell => {
    const columnDelta = Math.abs(cell.column - column)
    const rowDelta = Math.abs(cell.row - row)

    return columnDelta <= 1 && rowDelta <= 1 && columnDelta + rowDelta != 0
}).length

const isCellAlive = (grid, column, row) => {
    return grid.some(cell => cell.column == column && cell.row == row)
}

const getNextGeneration = (grid, startColumn, startRow, columns, rows) => {
    const next = []
    for (let row = startRow; row < startRow + rows; row++) {
        for (let column = startColumn; column < startColumn + columns; column ++) {
            const isAlive = isCellAlive(grid, column, row)
            const neighbors = getLiveNeighborCount(grid, column, row)
            if (willCellLive(isAlive, neighbors))
            {
                next.push(createLiveCell(column, row))
            }
        }
    }
    return next
}