import sys


tokens = [token.strip("'\"(),") for token in sys.stdin.read().split()]
if tokens:
	midpoint = len(tokens) // 2
	preorder = tokens[:midpoint]
	inorder = tokens[midpoint:]

	inorder_index = {value: index for index, value in enumerate(inorder)}

	def build(pre_start, in_start, in_end):
		if in_start >= in_end:
			return 0

		root_value = preorder[pre_start]
		root_index = inorder_index[root_value]
		left_size = root_index - in_start
		left_nodes = build(pre_start + 1, in_start, root_index)
		right_nodes = build(pre_start + left_size + 1, root_index + 1, in_end)

		# Count this node's child edges, plus the edges in both subtrees.
		return left_nodes + right_nodes + (left_size > 0) + (in_end - root_index - 1 > 0)

	print(build(0, 0, len(inorder)))