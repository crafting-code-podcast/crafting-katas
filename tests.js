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

test('the test harness shows successes correctly', () => {})

test('the test harness shows failures correctly', () => {
    fail('test failed')
})
