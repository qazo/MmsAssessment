<script setup lang="ts">
import { AxiosInstance } from 'axios';
import { TaskDto } from '../models'
import { PropType, inject, } from 'vue';

const emit = defineEmits({
	taskDeleted(task: TaskDto) { },
	taskEditRequested(task: TaskDto) { }
});

const props = defineProps({
	task: {
		type: Object as PropType<TaskDto>,
		required: true
	},
});
const apiClient = inject('http-client') as AxiosInstance;
const model = props.task;
if (model.isCompleted) {
	model.dateCompleted = new Date(model.dateCompleted!.toString());
}

if (model.description === '') {
	model.description = null;
}

function handleTaskDeletion(event: Event) {
	event.preventDefault();
	const task = props.task as TaskDto;
	apiClient.delete(`Tasks/${task.id}`)
		.then(resp => {
			const result = resp.data;
			if (!result.isSuccessful) {
				alert(result.message);
				return;
			}
			emit("taskDeleted", task);
		})
}

function handleTaskEdit(event: Event) {
	event.preventDefault();
	const task = props.task as TaskDto;
	emit("taskEditRequested", task);
}

function handleTaskComplete() {
	apiClient.put(`Tasks/Complete/${model.id}`)
		.then(resp => {
			const result = resp.data;
			if (!result.isSuccessful) {
				alert(result.message);
				return;
			}
			const resultTask = result.value as TaskDto;
			model.isCompleted = resultTask.isCompleted;
			model.dateCompleted = new Date(resultTask.dateCompleted!.toString());
		})
}

</script>

<template>
	<li class="list-group-item task-list-item p-0">
		<div class="row">
			<div class="col ps-4 p-2">
				<h6>
					{{ model.title }}
					<template v-if="model.isCompleted">
						<span :title="model.dateCompleted?.toLocaleString()"
							class="badge rounded-pill bg-success bg-opacity-50 ">Complete</span>
					</template>
				</h6>
				<small class="fst-italic text-black-50" v-text="model.description ?? 'No description'"></small>
			</div>
			<div class="col-auto">
				<div class="btn-group h-100">
					<button :disabled="model.isCompleted" type="button" @click="handleTaskComplete"
						class="btn btn-light rounded-0 border-start"
						:class="{ 'text-success': !model.isCompleted, 'text-secondary': model.isCompleted }"
						title="Complete">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
							class="bi bi-check-circle" viewBox="0 0 16 16">
							<path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
							<path
								d="m10.97 4.97-.02.022-3.473 4.425-2.093-2.094a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-1.071-1.05" />
						</svg>
					</button>
					<button :disabled="model.isCompleted" type="button" @click="handleTaskEdit"
						class="btn btn-light rounded-0 border-start" :class="{ 'text-secondary': model.isCompleted }"
						title="Edit">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
							class="bi bi-pencil-square" viewBox="0 0 16 16">
							<path
								d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
							<path fill-rule="evenodd"
								d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" />
						</svg>
					</button>
					<button type="button" @click="handleTaskDeletion" class="btn btn-light border-start text-danger"
						title="Delete">
						<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
							class="bi bi-trash" viewBox="0 0 16 16">
							<path
								d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" />
							<path
								d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z" />
						</svg>
					</button>
				</div>
			</div>
		</div>
	</li>
</template>

<style scoped>
.task-list-item:hover {
	background-color: rgb(240, 240, 240)
}

.task-list-item .btn:not(:hover) {
	background-color: transparent !important;
}
</style>