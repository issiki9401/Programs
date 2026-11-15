import shlex
import sys


def tree_height(preorder, inorder):
	if not preorder:
		return 0

	positions = {value: index for index, value in enumerate(inorder)}
	preorder_index = 0

	def height(left, right):
		nonlocal preorder_index
		if left > right:
			return 0

		root = preorder[preorder_index]
		preorder_index += 1
		split = positions[root]
		return 1 + max(height(left, split - 1), height(split + 1, right))

	return height(0, len(inorder) - 1)


def main():
	lines = [line for line in sys.stdin.read().splitlines() if line.strip()]
	if len(lines) < 2:
		return

	preorder = shlex.split(lines[0])
	inorder = shlex.split(lines[1])
	sys.setrecursionlimit(max(1000, len(preorder) * 2 + 10))
	print(tree_height(preorder, inorder))


if __name__ == "__main__":
	main()
