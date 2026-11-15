import shlex
import sys


lines = [line for line in sys.stdin if line.strip()]
if len(lines) < 2:
	print(0)
	raise SystemExit

preorder = shlex.split(lines[0])
inorder = shlex.split(lines[1])

positions = {value: index for index, value in enumerate(inorder)}
preorder_index = 0


def count_leaves(left, right):
	global preorder_index
	if left > right:
		return 0

	root = preorder[preorder_index]
	preorder_index += 1
	split = positions[root]

	if left == right:
		return 1

	return count_leaves(left, split - 1) + count_leaves(split + 1, right)


print(count_leaves(0, len(inorder) - 1))