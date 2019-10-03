import sys

def main(min, max):
    for word in sys.stdin.readlines():
        if len(word.strip()) >= min and len(word.strip()) <= max:
            print(word.strip())

if __name__ == "__main__":
    if len(sys.argv) == 3:
        main(int(sys.argv[1]), int(sys.argv[2]))
    if len(sys.argv) == 2:
        main(1, int(sys.argv[1]))
