export interface TaskDto {
	id: number;
	title: string;
	description: string | null;
	isCompleted: boolean;
	dateCompleted: Date | null;
};

export interface CreateTaskDto {
	title: string;
	description: string | null;
};

export interface UpdateTaskDto {
	id: number;
	title: string;
	description: string | null;
};