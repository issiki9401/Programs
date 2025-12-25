def can_form_square(sticks):
    total_length = sum(sticks)
    if len(sticks) < 4 or total_length % 4 != 0:
        return False
    
    target_side = total_length // 4
    
    if any(stick > target_side for stick in sticks):
        return False
    
    sticks.sort(reverse=True)
    
    sides = [0] * 4
    
    def backtrack(index):
        if index == len(sticks):
            return all(side == target_side for side in sides)
        
        current_stick = sticks[index]
        for i in range(4):
            if i > 0 and sides[i] == sides[i-1]:
                continue
            if sides[i] + current_stick > target_side:
                continue
            
            sides[i] += current_stick
            if backtrack(index + 1):
                return True
            sides[i] -= current_stick
            
            if sides[i] == 0:
                break
        
        return False
    
    return backtrack(0)

if __name__ == "__main__":
    import sys
    for line in sys.stdin:
        line = line.strip()
        if not line:
            continue
        sticks = list(map(int, line.split()))
        print("yes" if can_form_square(sticks) else "no")