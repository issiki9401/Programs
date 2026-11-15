import sys


def main():
	lines = [line.split() for line in sys.stdin if line.strip()]
	if len(lines) < 2:
		return

	preorder, inorder = lines[0], lines[1]
	positions = {value: index for index, value in enumerate(inorder)}
	next_preorder = 0
	postorder = []

	def traverse(left, right):
		nonlocal next_preorder
		if left >= right:
			return

		root = preorder[next_preorder]
		next_preorder += 1
		root_index = positions[root]
		traverse(left, root_index)
		traverse(root_index + 1, right)
		postorder.append(root)

	traverse(0, len(inorder))
	print(" ".join(postorder))


if __name__ == "__main__":
	main()
