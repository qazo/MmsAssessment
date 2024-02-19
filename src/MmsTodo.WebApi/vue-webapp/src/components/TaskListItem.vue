<script setup lang="ts">
import { TaskDto } from '../models'
import { reactive } from 'vue';

const emits = defineEmits({
	taskDeleted(task: TaskDto) { },
	taskUpdated(task: TaskDto) { }
});

const props = defineProps({
	task: {
		type: Object,
		required: true
	},
});
const model = reactive<TaskDto>(props.task as any)

function handleTaskDeletion(event: Event) {
	event.preventDefault();
}

function handleTaskEdit(event: Event) {
	event.preventDefault();
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