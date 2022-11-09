Array.from([0, 1]).forEach(x => test(`a live cell will die if it has ${x} neighbors`, () => {
    assert(willCellLive(true, x)).equals(false)
}))

Array.from([2, 3]).forEach(x => test(`a live cell will continue to live if it has ${x} neighbors`, () => {
    assert(willCellLive(true, x)).equals(true)
}))

Array.from([4, 5, 6, 7, 8]).forEach(x => test(`a live cell will die if it has ${x} neighbors`, () => {
    assert(willCellLive(true, x)).equals(false)
}))

test('A dead cell will be brought to life if it has exactly 3 living neighbors.', () => {
    assert(willCellLive(false, 3)).equals(true)
})

Array.from([0, 1, 2, 4, 5, 6, 7, 8]).forEach(x => test(`A dead cell stays dead with ${x} neighbors`, () => {
    assert(willCellLive(false, x)).equals(false)
}))

test('when creating a new grid', () => {
    assert(createNewGrid().length).equals(0)
})

test('when creating a live cell', () => {
    assert(createLiveCell(4, -6)).deepEquals({column: 4, row: -6})
})

test('when setting live cells', () => {
    const newGrid = createNewGrid();
    const cells = [
        createLiveCell(0, 0),
        createLiveCell(1, 4),
        createLiveCell(-3, 2)
    ]
    const expected = [
        {column: 0, row: 0},
        {column: 1, row: 4},
        {column: -3, row: 2}
    ]

    assert(setLiveCells(newGrid, cells)).deepEquals(expected)
    assert(newGrid).deepEquals([])
})

test('when setting live cells duplicates are handled', () => {
    const newGrid = createNewGrid();
    const cells = [
        createLiveCell(0, 0),
        createLiveCell(1, 4),
        createLiveCell(0, 0),
        createLiveCell(1, 4),
        createLiveCell(1, 4)
    ]
    const expected = [
        {column: 0, row: 0},
        {column: 1, row: 4}
    ]

    assert(setLiveCells(newGrid, cells)).deepEquals(expected)
})

Array.from([
    {column: 70, row: 80, expect: 0},
    {column: -5, row: -5, expect: 0},
    {column: 0, row: 0, expect: 1},
    {column: 1, row: 1, expect: 3},
    {column: 5, row: 5, expect: 5}
]).forEach(x => test(`when getting live neighbor count at (${x.column}, ${x.row}), expect ${x.expect}`, () => {
    const grid = setLiveCells(createNewGrid(), [
        createLiveCell(-5, -5),
        createLiveCell(0, 0),
        createLiveCell(1, 1),
        createLiveCell(1, 2),
        createLiveCell(2, 1),
        createLiveCell(5, 5),
        createLiveCell(4, 5),
        createLiveCell(5, 4),
        createLiveCell(4, 4),
        createLiveCell(4, 6),
        createLiveCell(6, 5)
    ])

    assert(getLiveNeighborCount(grid, x.column, x.row)).equals(x.expect)
}))

test('when checking if a cell is alive', () => {
    const grid = setLiveCells(createNewGrid(), [
        createLiveCell(0, 0),
        createLiveCell(1, 1),
        createLiveCell(1, 2)
    ])

    assert(isCellAlive(grid, 0, 0)).equals(true)
    assert(isCellAlive(grid, 0, 1)).equals(false)
    assert(isCellAlive(grid, 1, 1)).equals(true)
    assert(isCellAlive(grid, -1, 1)).equals(false)
})

const cellBlock = [
    createLiveCell(-1, -1),
    createLiveCell(-1, 0),
    createLiveCell(-1, 1),
    createLiveCell(0, -1),
    createLiveCell(0, 0),
    createLiveCell(0, 1),
    createLiveCell(1, -1),
    createLiveCell(1, 0),
    createLiveCell(1, 1),
]

test('when calculating the next generation in a 3x3 grid', () => {
    const grid = setLiveCells(createNewGrid(), cellBlock)

    assert(getNextGeneration(grid, -1, -1, 3, 3)).deepEquals([
        { column: -1, row: -1 },
        { column: 1, row: -1 },
        { column: -1, row: 1 },
        { column: 1, row: 1 }
    ])
})

test('when calculating the next generation in a 5x5 grid', () => {
    const grid = setLiveCells(createNewGrid(), cellBlock)

    assert(getNextGeneration(grid, -2, -2, 5, 5)).deepEquals([
        { column: 0, row: -2 },
        { column: -1, row: -1 },
        { column: 1, row: -1 },
        { column: -2, row: 0 },
        { column: 2, row: 0 },
        { column: -1, row: 1 },
        { column: 1, row: 1 },
        { column: 0, row: 2 }
    ])
})