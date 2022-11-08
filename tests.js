const tests = []

const runAllTests = () => {
    const container = document.getElementById("tests")
    let totalFailures = 0
    tests.forEach(test => {
        const result = runTest(test.action)

        const testName = document.createElement('p')
        testName.innerHTML = test.name

        const div = document.createElement('div')
        div.classList.add(result.success ? 'success' : 'failure')
        div.appendChild(testName)

        if (!result.success) {
            totalFailures++
            const errorDiv = document.createElement('div')
            errorDiv.innerHTML = result.error
            div.appendChild(errorDiv)
        }

        container.appendChild(div)
    })

    document.getElementById("totals").innerHTML = `Ran ${tests.length} tests and got ${totalFailures} failures`
}

const runTest = (action) => {
    try {
        action()
        return { success: true }
    }
    catch (error) {
        return { success: false, error }
    }
}

const test = (name, action) => tests.push({name, action})

const fail = (message) => { throw new Error(message) }

const assert = (value) => ({
    equals: (expected) => {
        if (value !== expected) {
            throw new Error(`Expected ${expected} but got ${value}`)
        }
    },
    deepEquals: (expected) => {
        const valueJson = JSON.stringify(value)
        const expectedJson = JSON.stringify(expected)
        if (valueJson !== expectedJson) {
            throw new Error(`Expected ${expectedJson} but got ${valueJson}`)
        }
    }
})

test('the test harness shows successes correctly', () => {})

test('the test harness shows failures correctly', () => {
    fail('example test failure')
})

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
        createLiveCell(4, 5),
        createLiveCell(6, 5)
    ])

    assert(getLiveNeighborCount(grid, x.column, x.row)).equals(x.expect)
}))