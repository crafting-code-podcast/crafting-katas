const willCellLive = (isAlive, livingNeighborsCount) => {
    if (isAlive && livingNeighborsCount === 2) {
        return true
    }
    if (livingNeighborsCount === 3) {
        return true
    }
    return false
}