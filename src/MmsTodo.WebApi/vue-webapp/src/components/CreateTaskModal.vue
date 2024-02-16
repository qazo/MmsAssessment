<script setup lang="ts">
import { reactive, ref } from 'vue'
import { CreateTaskDto, TaskDto } from '../models';

const emit = defineEmits({
	taskCreated(payload: TaskDto) { }
});
const isVisible = ref(false);
const formRef = ref<HTMLFormElement>();
const model = reactive<CreateTaskDto>({
	title: '',
	description: null
});


function handleSubmit(event: Event) {
	event.preventDefault();
	hideModal();
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
					<h6 class="modal-title">Create/Edit Task</h6>
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
					<button type="button" class="btn btn-primary" @click="handleSubmit">Create</button>
				</div>
			</div>
		</div>
	</div>
</template>