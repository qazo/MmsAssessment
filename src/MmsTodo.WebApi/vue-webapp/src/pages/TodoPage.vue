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
			<div class="col col-md-5 text-center">
				<div class="my-3">
					<button type="button" @click="showNewTaskModal" class="btn btn-primary w-100">New Task</button>
				</div>
			</div>
		</div>
		<div class="row my-3 justify-content-center">
			<div class="col col-md-5">
				<template v-if="taskList.length > 0">
					<TaskList :task-list="taskList" />
				</template>
				<template v-else>
					<div class="card text-bg-light">
						<div class="card-body text-center">
							<h5 class="card-title">No Tasks</h5>
							<h6 class="card-subtitle mb-2 text-body-secondary">Task list is empty.</h6>
							<p class="card-text">Click on the "New Task" button to add a new task</p>
						</div>
					</div>
				</template>
			</div>
		</div>
	</div>

	<CreateTaskModal :is-visible="newTaskModalVisible" @task-created="onTaskCreated"
		@modal-hidden="() => newTaskModalVisible = false" />
</template>