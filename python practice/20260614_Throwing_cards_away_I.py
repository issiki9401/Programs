import sys
from collections import deque

def main():
    input_data = sys.stdin.read().split()
    for item in input_data:
        n = int(item)
        if n == 0:
            break
        
        cards = deque(range(1, n + 1))
        discarded = []
        
        while len(cards) > 1:
            discarded.append(str(cards.popleft()))
            cards.append(cards.popleft())
            
        if discarded:
            print(f"Discarded cards: {', '.join(discarded)}")
        else:
            print("Discarded cards:")
            
        print(f"Remaining card: {cards[0]}")

if __name__ == '__main__':
    main()