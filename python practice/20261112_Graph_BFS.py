from collections import deque
import sys


def main():
	tokens = sys.stdin.read().split()
	if not tokens:
		return

	n = int(tokens[0])
	position = 1
	adjacency = []

	for _ in range(n):
		row_token = tokens[position]
		position += 1
		if len(row_token) == n and all(char in "01" for char in row_token):
			adjacency.append([int(char) for char in row_token])
		else:
			row = [int(row_token)]
			row.extend(int(tokens[position + i]) for i in range(n - 1))
			position += n - 1
			adjacency.append(row)

	start = int(tokens[position])
	visited = [False] * n
	visited[start] = True
	queue = deque([start])
	order = []

	while queue:
		node = queue.popleft()
		order.append(node)
		for neighbor in range(n):
			if adjacency[node][neighbor] and not visited[neighbor]:
				visited[neighbor] = True
				queue.append(neighbor)

	print(*order)


if __name__ == "__main__":
	main()
