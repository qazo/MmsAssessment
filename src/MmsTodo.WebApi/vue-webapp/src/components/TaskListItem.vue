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

function handleDeletion(event: Event) {
	event.preventDefault();
}

</script>

<template>
	<div class="row">
		<div class="col-10">
			<p>{{ model.title }}</p>
			<small class="text-body-secondary">{{ model.description ?? '' }}</small>
		</div>
		<div class="col-2">
			<button class="btn btn-secondary" title="Delete" @click="handleDeletion">X</button>
			<button class="btn btn-danger" title="Delete" @click="handleDeletion">X</button>
		</div>
	</div>
</template>