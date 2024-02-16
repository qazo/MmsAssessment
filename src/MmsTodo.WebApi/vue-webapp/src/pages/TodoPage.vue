<script setup lang="ts">
import { AxiosInstance } from 'axios';
import { inject, onMounted, reactive, ref } from 'vue';
import { TaskDto } from '../models';
import TaskListItem from '../components/TaskListItem.vue';


const apiClient = inject('http-client') as AxiosInstance;
const newTaskModalVisible = ref(false);
const tasksLoaded = ref(false);
const taskList = reactive<TaskDto[]>([])

function showNewTaskModal() {
	newTaskModalVisible.value = true;
}

function onTaskDeleted(task: TaskDto) {

}

function onTaskUpdated(task: TaskDto) {

}

function loadTasks() {
	apiClient.get('Tasks')
		.then(resp => {
			taskList.push(...resp.data.value);
			tasksLoaded.value = true;
		});
}

onMounted(async () => {
	loadTasks();
});
</script>

<template>
	<div class="container">
		<div class="row">
			<div class="col col-md-6">
				<div class="my-3">
					<button type="button" @click="showNewTaskModal" class="btn btn-primary">New Task</button>
				</div>

				<div v-for="task in taskList">
					<TaskListItem :task="task" @task-deleted="onTaskDeleted" @task-updated="onTaskUpdated" />
				</div>
			</div>
		</div>
	</div>
</template>