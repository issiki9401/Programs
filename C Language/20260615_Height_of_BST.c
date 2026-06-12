#include <stdio.h>
#include <stdlib.h>

typedef struct Node {
    int val;
    struct Node *left, *right;
} Node;

Node* insert(Node* root, int val) {
    if (!root) {
        Node* node = (Node*)malloc(sizeof(Node));
        node->val = val;
        node->left = node->right = NULL;
        return node;
    }
    if (val < root->val) {
        root->left = insert(root->left, val);
    } else {
        root->right = insert(root->right, val);
    }
    return root;
}

int height(Node* root) {
    if (!root) return 0;
    int left_h = height(root->left);
    int right_h = height(root->right);
    return 1 + (left_h > right_h ? left_h : right_h);
}

int main() {
    int n;
    while (scanf("%d", &n) == 1) {
        Node* root = NULL;
        for (int i = 0; i < n; i++) {
            int val;
            scanf("%d", &val);
            root = insert(root, val);
        }
        printf("%d\n", height(root));
    }
    return 0;
}