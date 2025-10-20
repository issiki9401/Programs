#include <iostream>
#include <stack>
#include <string>
#include <unordered_map>

using namespace std;

bool isValidParentheses(const string& s) {
    stack<char> st;
    unordered_map<char, char> mapping = {
        {')', '('},
        {']', '['},
        {'>', '<'},
        {'}', '{'}
    };

    for (char c : s) {
        if (mapping.find(c) != mapping.end()) {
            if (st.empty()) { // 檢查堆疊是否為空
                return false;
            }
            if (mapping[c] != st.top()) {
                return false;
            }
            st.pop();
        } else if (c == '(' || c == '[' || c == '<' || c == '{') {
            st.push(c);
        }
    }

    return st.empty();
}

int main() {
    int t;
    cin >> t;
    cin.ignore();

    while (t--) {
        string s;
        getline(cin, s);
        if (isValidParentheses(s)) {
            cout << 'Y' << endl;
        } else {
            cout << 'N' << endl;
        }
    }

    return 0;
}