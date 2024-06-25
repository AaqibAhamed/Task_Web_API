import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Task } from '../models/task.model';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [NgIf],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css'
})
export class TaskFormComponent {

  task: Task = new Task;

  constructor(private taskService: TaskService) { }

  saveTask(): void {
    if (this.task.id) {
      this.taskService.updateTask(this.task).subscribe(() => this.task);
    } else {
      this.taskService.createTask(this.task).subscribe(() => this.task);
    }
  }
}
