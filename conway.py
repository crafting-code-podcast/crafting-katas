import json
from pprint import pprint

def test_get_top_left_neighbor_state():
    grid = [["$","o","o"],["o","o","o"],["o","o","o"]]
    row = 1
    cell = 1
    top_left_neighbor_state = get_top_left_neighbor_state(grid, row, cell)
    state = "pass" if top_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","o","o"],["o","o","$"]]
    row = 0
    cell = 0
    top_left_neighbor_state = get_top_left_neighbor_state(grid, row, cell)
    state = "pass" if top_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","$","o"],["o","o","o"],["o","o","o"]]
    row = 1
    cell = 2
    top_left_neighbor_state = get_top_left_neighbor_state(grid, row, cell)
    state = "pass" if top_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","o","o"],["$","o","o"]]
    row = 0
    cell = 1
    top_left_neighbor_state = get_top_left_neighbor_state(grid, row, cell)
    state = "pass" if top_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_top_neighbor_state():
    grid = [["o","$","o"],["o","o","o"],["o","o","o"]]
    row = 1
    cell = 1
    top_neighbor_state = get_top_neighbor_state(grid, row, cell)
    state = "pass" if top_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","o","$"],["o","o","o"]]
    row = 2
    cell = 2
    top_neighbor_state = get_top_neighbor_state(grid, row, cell)
    state = "pass" if top_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_top_right_neighbor_state():
    grid = [["o","o","$"],["o","o","o"],["o","o","o"]]
    row = 1
    cell = 1
    top_right_neighbor_state = get_top_right_neighbor_state(grid, row, cell)
    state = "pass" if top_right_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","$","o"],["o","o","o"]]
    row = 2
    cell = 0
    top_right_neighbor_state = get_top_right_neighbor_state(grid, row, cell)
    state = "pass" if top_right_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["$","o","o"],["o","o","o"],["o","o","o"]]
    row = 1
    cell = 2
    top_right_neighbor_state = get_top_right_neighbor_state(grid, row, cell)
    state = "pass" if top_right_neighbor_state == "$" else "fail"
    yield {"name": test_get_top_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

def test_get_right_neighbor_state():
    grid = [["o","o","$"],["o","o","o"],["o","o","o"]]
    row = 0
    cell = 1
    right_neighbor_state = get_right_neighbor_state(grid, row, cell)
    state = "pass" if right_neighbor_state == "$" else "fail"
    yield {"name": test_get_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["$","o","o"],["o","o","o"],["o","o","o"]]
    row = 0
    cell = 2
    right_neighbor_state = get_right_neighbor_state(grid, row, cell)
    state = "pass" if right_neighbor_state == "$" else "fail"
    yield {"name": test_get_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_bottom_right_neighbor_state():
    grid = [["o","o","o"],["o","o","o"],["o","o","$"]]
    row = 1
    cell = 1
    bottom_right_neighbor_state = get_bottom_right_neighbor_state(grid, row, cell)
    state = "pass" if bottom_right_neighbor_state == "$" else "fail"
    yield {"name": test_get_bottom_right_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_bottom_neighbor_state():
    grid = [["o","$","o"],["o","o","o"],["o","o","o"]]
    row = 2
    cell = 1
    bottom_neighbor_state = get_bottom_neighbor_state(grid, row, cell)
    state = "pass" if bottom_neighbor_state == "$" else "fail"
    yield {"name": test_get_bottom_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_bottom_left_neighbor_state():
    grid = [["o","o","$"],["o","o","o"],["o","o","o"]]
    row = 2
    cell = 0
    bottom_left_neighbor_state = get_bottom_left_neighbor_state(grid, row, cell)
    state = "pass" if bottom_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_bottom_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","o","o"],["$","o","o"]]
    row = 1
    cell = 1
    bottom_left_neighbor_state = get_bottom_left_neighbor_state(grid, row, cell)
    state = "pass" if bottom_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_bottom_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}

    grid = [["o","o","o"],["o","$","o"],["o","o","o"]]
    row = 0
    cell = 2
    bottom_left_neighbor_state = get_bottom_left_neighbor_state(grid, row, cell)
    state = "pass" if bottom_left_neighbor_state == "$" else "fail"
    yield {"name": test_get_bottom_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_get_left_neighbor_state():
    grid = [["o","o","o"],["$","o","o"],["o","o","o"]]
    row = 1
    cell = 1
    left_neighbor_state = get_left_neighbor_state(grid, row, cell)
    state = "pass" if left_neighbor_state == "$" else "fail"
    yield {"name": test_get_left_neighbor_state.__name__,"state": state, "row": row, "cell": cell}


def test_cell_state():
    current_state = "o"
    neighbors = ["x","x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "x"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "o"
    neighbors = ["x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "o"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "o"
    neighbors = ["x","x","x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "o"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "x"
    neighbors = ["x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "o"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "x"
    neighbors = ["x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "x"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "x"
    neighbors = ["x","x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "x"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}

    current_state = "x"
    neighbors = ["x","x","x","x"]
    cell_state = get_cell_state(current_state, neighbors)
    expected  = "o"
    state = "pass" if cell_state == expected else "fail"
    yield {"name": test_cell_state.__name__,"state": state, "current_state": current_state, "neighbors": neighbors}


def run_all_tests():
    results = []

    tests = [
        test_get_top_left_neighbor_state,
        test_get_top_neighbor_state,
        test_get_top_right_neighbor_state,
        test_get_right_neighbor_state,
        test_get_bottom_right_neighbor_state,
        test_get_bottom_neighbor_state,
        test_get_bottom_left_neighbor_state,
        test_get_left_neighbor_state,
        test_cell_state
    ]

    for test in tests:
        for result in test():
            results.append(result)

    failures = [result for result in results if result["state"] == "fail"]
    print(f"ran {len(results)} tests, {len(failures)} failures")
    pprint(failures)
    

def get_left_neighbor_state(grid, row, cell):
    cell = 2 if cell == 0 else cell-1
    return grid[row][cell]


def get_bottom_left_neighbor_state(grid, row, cell):
    row = 0 if row == 2 else row+1
    cell = 2 if cell == 0 else cell-1
    return grid[row][cell]


def get_bottom_neighbor_state(grid, row, cell):
    row = 0 if row == 2 else row+1
    return grid[row][cell]


def get_bottom_right_neighbor_state(grid, row, cell):
    row = 0 if row == 2 else row+1
    cell = 0 if cell == 2 else cell+1
    return grid[row][cell]


def get_right_neighbor_state(grid, row, cell):
    cell = 0 if cell == 2 else cell+1
    return grid[row][cell]


def get_top_left_neighbor_state(grid, row, cell):
    row = 2 if row == 0 else row-1
    cell = 2 if cell == 0 else cell-1
    return grid[row][cell]


def get_top_neighbor_state(grid, row, cell):
    row = 2 if row == 0 else row-1
    return grid[row][cell]


def get_top_right_neighbor_state(grid, row, cell):
    row = 2 if row == 0 else row-1
    cell = 0 if cell == 2 else cell+1
    return grid[row][cell]


def get_cell_state(current_state, neighbors):
    living_neighbor_count = len([neighbor for neighbor in neighbors if neighbor == "x"])

    if current_state == "o":
        return "x" if living_neighbor_count == 3 else "o"
    else:
        if living_neighbor_count < 2:
            return "o"
        elif living_neighbor_count == 2 or living_neighbor_count == 3:
            return "x"
        else:
            return "o"
        
    
def generate(grid):
    next_generation = default_grid()

    for row in range(len(grid)):
        for cell in range(len(grid[row])):
            neighbors = []
            neighbors.append(get_top_left_neighbor_state(grid, row, cell))
            neighbors.append(get_top_neighbor_state(grid, row, cell))
            neighbors.append(get_top_right_neighbor_state(grid, row, cell))
            neighbors.append(get_right_neighbor_state(grid, row, cell))
            neighbors.append(get_bottom_right_neighbor_state(grid, row, cell))
            neighbors.append(get_bottom_neighbor_state(grid, row, cell))
            neighbors.append(get_bottom_left_neighbor_state(grid, row, cell))
            neighbors.append(get_left_neighbor_state(grid, row, cell))

            next_generation[row][cell] = get_cell_state(grid[row][cell], neighbors)

    return next_generation


def default_grid():
    return [["o","o","o"],["o","o","o"],["o","o","o"]]
