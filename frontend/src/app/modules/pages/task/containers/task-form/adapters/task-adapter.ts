export class TaskAdapter {
  id?: string;
  name: string;
  category: string;
  limitDate: string;

  constructor(task: any) {
    ({ name: this.name, category: this.category, id: this.id } = task);

    this.limitDate = new Date(task.limitDate).toString();
  }
}

export class TaskOutputAdapter {
  taskId?: string;
  name: string;
  category: string;
  limitDate: Date;

  constructor(task: any) {
    ({
      name: this.name,
      category: this.category,
      limitDate: this.limitDate,
      id: this.taskId,
    } = task);
  }
}
