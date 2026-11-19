import sys
from decimal import Decimal

for line in sys.stdin:
  line = line.strip().lstrip('\ufeff')
  if not line:
    continue

  tokens = [x.strip() for x in line.split(',') if x.strip()]
  if tokens:
    tokens.sort(key=Decimal, reverse=True)
    print(' '.join(tokens))