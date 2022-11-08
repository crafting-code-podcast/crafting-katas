const tests = []

const runAllTests = () => {
    const container = document.getElementById("tests")
    tests.forEach(test => {
        const result = runTest(test.action)

        const testName = document.createElement('p')
        testName.innerHTML = test.name

        const div = document.createElement('div')
        div.classList.add(result.success ? 'success' : 'failure')
        div.appendChild(testName)

        if (!result.success) {
            const errorDiv = document.createElement('div')
            errorDiv.innerHTML = result.error
            div.appendChild(errorDiv)
        }

        container.appendChild(div)
    })
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