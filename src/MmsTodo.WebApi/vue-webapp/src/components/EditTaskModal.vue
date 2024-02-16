<script setup lang="ts">
import { inject, reactive, ref } from 'vue'
import { UpdateTaskDto, TaskDto } from '../models';
import { AxiosInstance } from 'axios';

const emit = defineEmits({
	taskUpdated(payload: TaskDto) { }
});
const isVisible = ref(false);
const formRef = ref<HTMLFormElement>();

const props = defineProps({
	task: {
		type: Object,
		required: true,
	}
})

const model = reactive<UpdateTaskDto>(props.task as any);
const apiClient = inject('axios-client') as AxiosInstance;

function handleSubmit(event: Event) {
	event.preventDefault();
	apiClient.put('Tasks', model)
		.then(resp => {
			if (!resp.data.isSuccessful) {
				alert(resp.data.message);
				return;
			}
			const updatedTask = resp.data.value as TaskDto;
			emit('taskUpdated', updatedTask);
			hideModal();
		});
}

function hideModal() {
	isVisible.value = false;
	formRef.value?.reset();
}

</script>

<template>
	<div class="modal" tabindex="-1">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-header">
					<h6 class="modal-title">Edit Task</h6>
				</div>
				<div class="modal-body">
					<form ref="formRef">
						<div class="mb-3">
							<label for="title" class="col-form-label">Title</label>
							<input required v-model="model.title" type="text" id="title" class="form-control">
						</div>
						<div class="mb-3">
							<label for="description" class="col-form-label">Description</label>
							<textarea v-model="model.description" id="description" class="form-control"></textarea>
						</div>
					</form>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" @click="hideModal">Cancel</button>
					<button type="button" class="btn btn-primary" @click="handleSubmit">Update</button>
				</div>
			</div>
		</div>
	</div>
</template>