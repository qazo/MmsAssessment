<script setup lang="ts">
import { reactive, ref } from 'vue';
import TaskListItem from './TaskListItem.vue';
import { TaskDto } from '../models';
import EditTaskModal from './EditTaskModal.vue';

const props = defineProps({
	taskList: {
		type: Array,
		required: true
	}
});

const emit = defineEmits({
	taskEditRequested(task: TaskDto) { },
})

const isEditVisible = ref(false);
const currentEditTask = ref<TaskDto | null>(null);
function onTaskDeleted(task: TaskDto) {

}

function onTaskUpdated(task: TaskDto) {

}

function onTaskEditRequested(task: TaskDto) {
	currentEditTask.value = task;
	isEditVisible.value = true;
}

const taskList = reactive(props.taskList as TaskDto[])
</script>

<template>
	<ul class="list-group">
		<template v-for="task in taskList">
			<TaskListItem :task="task" @task-deleted="onTaskDeleted" @task-edit-requested="onTaskEditRequested" />
		</template>
	</ul>

	<template v-if="currentEditTask != null">
		<EditTaskModal :is-visible="isEditVisible" :task="currentEditTask" @task-updated="onTaskUpdated"
			@modal-hidden="() => isEditVisible = false" />
	</template>
</template>