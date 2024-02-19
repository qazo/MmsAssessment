<script setup lang="ts">
import { AxiosInstance } from 'axios';
import { TaskDto } from '../models'
import { inject, reactive } from 'vue';

const emit = defineEmits({
	taskDeleted(task: TaskDto) { },
	taskEditRequested(task: TaskDto) { }
});

const props = defineProps({
	task: {
		type: Object,
		required: true
	},
});
const apiClient = inject('http-client') as AxiosInstance;
const model = reactive<TaskDto>(props.task as TaskDto)

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

</script>

<template>
	<li class="list-group-item p-0">
		<div class="row">
			<div class="col ps-4 p-2">
				<h6 v-text="model.title"></h6>
				<small v-text="model.description"></small>
			</div>
			<div class="col-auto p-0">
				<div class="btn-group h-100">
					<button type="button" @click="handleTaskEdit" class="btn btn-warning rounded-0">E</button>
					<button type="button" @click="handleTaskDeletion" class="btn btn-danger">D</button>
				</div>
			</div>
		</div>
	</li>
</template>