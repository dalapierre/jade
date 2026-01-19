using System.Collections.Generic;

class UndoableQueue<T>
{
    private Queue<T> queue;
    private Stack<T> undoStack;

    public UndoableQueue() {
        queue = new Queue<T>();
        undoStack = new Stack<T>();
    }

    public void Enqueue(T item) {
        queue.Enqueue(item);
        undoStack.Push(item);
    }

    public T Dequeue() {
        return queue.Dequeue();
    }

    public void Undo() {
        if (undoStack.Count == 0) {
            return;
        }

        T item = undoStack.Pop();
        queue.Dequeue(); // Remove the corresponding item from the queue
    }
}