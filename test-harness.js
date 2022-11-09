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
