<script setup lang="ts">
import { AxiosInstance } from 'axios';
import { inject, onMounted, reactive, ref } from 'vue';
import { TaskDto } from '../models';
import TaskList from '../components/TaskList.vue';
import CreateTaskModal from '../components/CreateTaskModal.vue';


const apiClient = inject('http-client') as AxiosInstance;
const newTaskModalVisible = ref(false);
const tasksLoaded = ref(false);
const taskList = reactive<TaskDto[]>([])
function showNewTaskModal() {
	newTaskModalVisible.value = true;
}

function loadTasks() {
	apiClient.get('Tasks')
		.then(resp => {
			taskList.push(...resp.data);
			tasksLoaded.value = true;
		});
}

function onTaskCreated(newTask: TaskDto) {
	taskList.push(newTask);
}

onMounted(async () => {
	loadTasks();
});
</script>

<template>
	<div class="container w-100 p-3">
		<div class="row my-3 justify-content-center">
			<div class="col col-md-6 text-center">
				<div class="my-3">
					<button type="button" @click="showNewTaskModal" class="btn btn-primary w-100">New Task</button>
				</div>
			</div>
		</div>
		<div class="row my-3 justify-content-center">
			<div class="col col-md-6">
				<TaskList :task-list="taskList" />
			</div>
		</div>
	</div>

	<dialog open>
		<h6>This is it</h6>
	</dialog>

	<CreateTaskModal :is-visible="newTaskModalVisible" @task-created="onTaskCreated"
		@modal-hidden="() => newTaskModalVisible = false" />
</template>